import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

export interface FnbTable {
  Id?: number;
  Name: string;
  SeatCount: number;
  Location?: string;
  Status: number;
  MerchantId: number;
}

@Injectable({
  providedIn: 'root',
})
export class FnbTableDataService {
  private apiUrl = environment.apiUrl + '/fnb-table';

  constructor(private http: HttpClient) {}

  getTables(): Observable<FnbTable[]> {
    return this.http.get<FnbTable[]>(this.apiUrl);
  }

  createTable(data: FnbTable): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }

  updateTable(id: number, data: FnbTable): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, data);
  }
}
