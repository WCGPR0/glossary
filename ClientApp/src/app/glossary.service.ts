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

  getTerms(): Observable<string[]> {
    return this.http.get<string[]>(this.url + "/GetTerms");
  }
  getDefinition(term: string): Observable<string> {
    const params = new HttpParams().set('term', term);
    return this.http.get<string>(this.url + "/GetDefinition", { params : params });
  }
  updateTerm(term: string, definition: string): Observable<number> {
    const params = new HttpParams().set('term', term).set('definition', definition);
    console.log(params);
    return this.http.put<number>(this.url + "/UpdateTerm", {}, { params : params });
  }

}
