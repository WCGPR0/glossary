import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { Glossary } from './glossary.model';
import { GlossaryService } from './glossary.service';
import {map, startWith} from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  filteredTerms: Observable<string[]>;
  terms: string[] = [];
  myControl = new FormControl();

  term: string = "Term";
  definition: string = "Definition";
  title = 'Glossary';
  constructor(private glossaryService: GlossaryService) { }

  ngOnInit() {
    this.glossaryService.getTerms().subscribe((terms: string[]) => this.terms = terms); 
    this.filteredTerms = this.myControl.valueChanges.pipe(startWith(''), map(value => this._filter(value)));
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.terms.filter(term => term.toLowerCase().includes(filterValue));
  }

  OnUpdateTerm() {
    this.glossaryService.getDefinition(this.term).subscribe(definition => this.definition = definition);
  }

  OnUpdateDefinition(definition: string) {
    var returnCode: number;
    this.glossaryService.updateTerm(this.term, this.definition).subscribe(_returnCode => returnCode = _returnCode);
  }

}
