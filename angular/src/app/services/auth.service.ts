import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OakellAuthService {
  private apiUrl = environment.apis.default.url;

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    const body = new HttpParams()
      .set('client_id', 'Oakell_App')
      .set('grant_type', 'password')
      .set('username', username)
      .set('password', password)
      .set('client_secret', '1q2w3e*');

    return this.http.post(`${this.apiUrl}/connect/token`, body, {
      headers: new HttpHeaders({
        'Content-Type': 'application/x-www-form-urlencoded'
      })
    });
  }
}
