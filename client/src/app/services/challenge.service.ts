import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Challenge } from '../models/challenge';

const baseUrl = `${environment.apiUrl}`;

@Injectable({ providedIn: 'root' })
export class ChallengeService {
    constructor(private http: HttpClient) { }

    submitTask(params: Challenge): Observable<Challenge>{
        return this.http.post<Challenge>(`${baseUrl}/challenge/submitTask`, params);
    }
}
