import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import './rxjs-extensions';

import { AppComponent }  from './app.component';
import { LegionComponent } from './legion.component';

@NgModule({
  imports: [ BrowserModule, HttpModule ],
  declarations: [ AppComponent, LegionComponent ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
