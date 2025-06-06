import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
// Adjust the import path if your environment file is in a different location
import { environment } from '../environments/environment';

export interface CategoryAddRequest {
  Name: string;
}

@Injectable({
  providedIn: 'root',
})
export class CategoryDataService {
  private apiUrl = environment.apiUrl + '/category';

  constructor(private http: HttpClient) {}

  createCategory(data: CategoryAddRequest): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }
}
