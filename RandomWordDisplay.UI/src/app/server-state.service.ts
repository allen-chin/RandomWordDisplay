import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ServerState } from './server-state.model'

@Injectable({
  providedIn: 'root'
})
export class ServerStateService {
  private baseUrl = "https://localhost:5000/api/State";

  constructor(private http: HttpClient) { }

  getServerState(): Observable<ServerState> {
    const url = `${this.baseUrl}/`;
    console.log(this.http.get<ServerState>(url));
    return this.http.get<ServerState>(url);
  }
}
