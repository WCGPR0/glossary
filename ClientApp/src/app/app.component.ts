import { Component } from '@angular/core';

import { Glossary } from './glossary.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  term: string = "Term";
  definition: string = "Definition";
  title = 'Glossary';

  OnUpdateTerm() {
    //@todo
  }

  OnUpdateDefinition() {
    //@todo
  }

}
