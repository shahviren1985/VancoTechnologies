import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { _throw } from 'rxjs/observable/throw';
import { Router } from '@angular/router';

@Injectable()
export class AuthenticationService {
    constructor(
        private router: Router,
        private http: HttpClient) { }

    login(phoneNumber: number, password: string) {
        return this.http.post<any>(`${environment.apiUrl}account/login`, { PhoneNumber: phoneNumber, Password: password })
            .pipe(map(result => {
                // return result;
                // login successful if there's a jwt token in the response
                if (result && result.Status) {
                    // store Username details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('token', result.Data.token);
                    // return new HttpResponse({ status: 200 });
                    return result;
                }
                else {
                    return result;
                }
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('token');
        this.router.navigate(['/login'])
    }
}