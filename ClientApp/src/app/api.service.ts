import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Lead } from './models';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  constructor(private http: HttpClient) { }
  getLeads = () => this.http.get<Lead[]>(`${environment.apiUrl}/lead`);
  getLeadById = (id:string) => this.http.get<Lead>(`${environment.apiUrl}/lead/${id}`);
  createLead = (record:Lead) => this.http.post<Lead>(`${environment.apiUrl}/lead`,record);
  updateLead = (record:Lead) => this.http.put<Lead>(`${environment.apiUrl}/lead`,record);
  deleteLead = (id:string) => this.http.delete(`${environment.apiUrl}/lead`);
}
