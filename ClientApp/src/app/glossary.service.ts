import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Glossary } from './glossary.model';

@Injectable({
  providedIn: 'root'
})
export class GlossaryService {
  url = 'https://localhost:5001/api/Glossary';
  constructor(private http: HttpClient) { }

  getTerms(): Observable<string[] > {
    return this.http.get<string[]>(this.url + "/GetGlossary");
  }
}
