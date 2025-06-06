import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
// Adjust the import path if your environment file is in a different location
import { environment } from '../environments/environment';

export interface ProductGroupRequest {
  groupName: string;
  groupCode?: string;
}

@Injectable({
  providedIn: 'root',
})
export class ProductGroupDataService {
  private apiUrl = environment.apiUrl + '/productgroup';

  constructor(private http: HttpClient) {}

  createProductGroup(data: ProductGroupRequest): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }
}
