import { Component, OnInit } from '@angular/core';

import { Glossary } from './glossary.model';
import { GlossaryService } from './glossary.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  cachedGlossaryList: Glossary[];

  term: string = "Term";
  definition: string = "Definition";
  title = 'Glossary';

  constructor(private glossaryService: GlossaryService) { }

  ngOnInit() {
    this.glossaryService.getTerms().subscribe(terms => this.cachedGlossaryList = terms.map(term => new Glossary(term)));
  }

  OnUpdateTerm() {
    //@todo
  }

  OnUpdateDefinition() {
    //@todo
  }

}
