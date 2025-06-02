import { Injectable } from '@angular/core';
import {
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest,
    HttpErrorResponse,
    HttpStatusCode
} from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { NgxSpinnerService } from 'ngx-spinner';
import { HotToastService } from '@ngxpert/hot-toast'; // Import HotToastService

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
    constructor(
        private router: Router,
        private spinner: NgxSpinnerService,
        private toast: HotToastService // Inject HotToastService
    ) { }

    private token: string | null = null;

    intercept(
        req: HttpRequest<any>,
        next: HttpHandler
    ): Observable<HttpEvent<any>> {
        // Show spinner before request
        this.spinner.show();

        const token = this.getToken();
        const clonedRequest = token
            ? req.clone({
                setHeaders: {
                    Authorization: `Bearer ${token}`
                }
            })
            : req;

        return next.handle(clonedRequest).pipe(
            catchError((error: HttpErrorResponse) => {
                if (error.status === HttpStatusCode.Unauthorized) {
                    window.location.href = '/login';
                    return throwError(() => error);
                }
                this.toast.show(error.error?.message || 'Đã xảy ra lỗi máy chủ (500).');
                console.error('HTTP Error:', error);
                return throwError(() => error);
            }),
            finalize(() => {
                // Hide spinner after request completes or errors
                this.spinner.hide();
            })
        );
    }

    private getToken(): string | null {
        if (!this.token) {
            let session = localStorage.getItem('user_session');
            if (session) {
                let sessionData = JSON.parse(session);
                this.token = sessionData?.Token || null;
            }
        }
        return this.token;
    }
}