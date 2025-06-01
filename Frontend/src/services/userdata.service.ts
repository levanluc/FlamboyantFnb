import { Injectable } from '@angular/core';
import { HttpService } from '../utilities/http-service';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { BaseResponse } from '../models/responses/base-response.model';
import User from '../entities/user';
import { UserLoginRequest } from '../models/requests/user-login-request.model';

@Injectable({
    providedIn: 'root',
})
export default class UserDataService {
    private apiUrl = environment.apiUrl;

    constructor(private httpService: HttpService) { }

    login(request: UserLoginRequest): Observable<BaseResponse<User>> {
        // Use the backend URL from the environment file
        var header = new HttpHeaders();
        return this.httpService.post<BaseResponse<User>>(
            `${this.apiUrl}/login`,
            request
        );
    }
}