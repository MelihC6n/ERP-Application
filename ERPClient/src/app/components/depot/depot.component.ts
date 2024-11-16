import { Component } from '@angular/core';
import { SharedModule } from '../../modules/shared.module';
import { DepotModel } from '../../models/depot.model';
import { DepotPipe } from '../../pipes/depot.pipe';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-depot',
  standalone: true,
  imports: [SharedModule,DepotPipe],
  templateUrl: './depot.component.html',
  styleUrl: './depot.component.css'
})
export class DepotComponent {
search:string="";
depots:DepotModel[]=[];

createModel:DepotModel=new DepotModel();
updateModel:DepotModel=new DepotModel();

constructor(
  private http:HttpService,
  private swal:SwalService
) {}

create(form:NgForm){
  if(form.valid){
    
  }
}
}
