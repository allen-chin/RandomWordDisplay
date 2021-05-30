import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { ServerState } from './server-state.model'

@Injectable({
  providedIn: 'root'
})
export class ServerStateService {
  private baseUrl = "https://localhost:5001/api";

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  // TODO: Replace this with web sockets
  getServerState(): Observable<ServerState> {
    const url = `${this.baseUrl}/State`;
    return this.http.get<ServerState>(url);
  }

  postStartCommand(words: string[]): void {
    const url = `${this.baseUrl}/Start`;
    const body = JSON.stringify(words);
    const result = this.http.post<void>(url, body, this.httpOptions)
      .pipe(
        tap(_ => console.log(`Successfully posted start command.`)),
        catchError(this.handleError<string>('postStartCommand'))
      ).subscribe();
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      console.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }
}
