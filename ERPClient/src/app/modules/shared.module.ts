import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BlankComponent } from '../components/blank/blank.component';
import { SectionComponent } from '../components/section/section.component';
import {FormValidateDirective} from 'form-validate-angular';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BlankComponent,
    SectionComponent,
    FormsModule,
    FormValidateDirective
  ],
  exports:[
    CommonModule,
    BlankComponent,
    SectionComponent,
    FormsModule,
    FormValidateDirective
  ]
})
export class SharedModule { }
