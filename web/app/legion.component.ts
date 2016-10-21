import { Component } from '@angular/core';
import { Person, Legion, LegionService } from './legion.service';

@Component({
  moduleId: module.id,
  selector: 'my-legion',
  templateUrl: 'legion.component.html',
  styles: ['li {cursor: pointer;} .error {color:red;}'],
  providers: [LegionService]
})
export class LegionComponent {
  errorMessage: string;
  legion: Legion;

  constructor(private legionService: LegionService) { }

  getLegion() {
    this.legionService.getLegion()
      .subscribe(
        legion => this.legion = legion,
        error =>  this.errorMessage = <any>error
    );
  }

  ngOnInit() { this.getLegion(); }

}