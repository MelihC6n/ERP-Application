import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
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
export class DepotComponent implements OnInit {
search:string="";
depots:DepotModel[]=[];

createModel:DepotModel=new DepotModel();
updateModel:DepotModel=new DepotModel();

@ViewChild("createModelCloseBtn") createModelCloseBtn : ElementRef<HTMLButtonElement> | undefined;
@ViewChild("updateModelCloseBtn") updateModelCloseBtn : ElementRef<HTMLButtonElement> | undefined;

constructor(
  private http:HttpService,
  private swal:SwalService
) {}
ngOnInit(): void {
  this.getAll();
}

getAll(){
  this.http.post<DepotModel[]>("Depot/GetAll",{},res=>
  {
    this.depots=res;
  }
  )
}

create(form:NgForm){
  if(form.valid){
    this.http.post<string>("Depot/Create",this.createModel,res=>{
      this.swal.callToast(res);
      this.createModelCloseBtn?.nativeElement.click();
      this.createModel=new DepotModel();
      this.getAll();
    })
  }
}

get(depot:DepotModel){
  this.updateModel={...depot};
}

update(form:NgForm){
  if(form.valid){
    this.http.post<string>("Depot/Update",this.updateModel,res=>
    {
      this.swal.callToast(res);
      this.updateModelCloseBtn?.nativeElement.click();
      this.getAll();
    }
    )
  }
}

delete(depot:DepotModel){
  this.swal.callSwal("Silme Onayı!",`${depot.name} isimli depoyi silmek istiyor musunuz?`,()=>{
    this.http.post<string>("Depot/Delete",{id:depot.id},res=>{
      this.swal.callToast(res);
      this.getAll();
    })
  })
}
}
