import { Component, ElementRef, OnInit, ViewChild, viewChild, viewChildren } from '@angular/core';
import { SharedModule } from '../../modules/shared.module';
import { CustomerModel } from '../../models/customer.model';
import { HttpService } from '../../services/http.service';
import { CustomerPipe } from '../../pipes/customer.pipe';
import { NgForm } from '@angular/forms';
import { SwalService } from '../../services/swal.service';

@Component({
  selector: 'app-customers',
  standalone: true,
  imports: [SharedModule,CustomerPipe],
  templateUrl: './customers.component.html',
  styleUrl: './customers.component.css'
})
export class CustomersComponent implements OnInit{
customers:CustomerModel[] = [];
createModel:CustomerModel=new CustomerModel;
updateModel:CustomerModel=new CustomerModel;
search:string="";

@ViewChild("createModelCloseBtn") createModelCloseBtn: ElementRef<HTMLButtonElement> | undefined;
@ViewChild("updateModelCloseBtn") updateModelCloseBtn: ElementRef<HTMLButtonElement> | undefined;

constructor(
  private http:HttpService,
  private swal:SwalService
) {}

ngOnInit(): void {
  this.getAll();
}

getAll(){
  this.http.post<CustomerModel[]>("Customer/GetAll",{},res=>{
    this.customers=res;
  })
}

create(form:NgForm){
  if(form.valid){
    this.http.post<string>("Customer/Create",this.createModel,res=>{
      this.swal.callToast(res);
      this.createModel=new CustomerModel();
      this.createModelCloseBtn?.nativeElement.click();
      this.getAll();
    })
  }
}

delete(customer:CustomerModel){
  this.swal.callSwal("Silme Onayı!",`${customer.name} isimli müşteriyi silmek istiyor musunuz? \n Dikkat bu işlem geri alınamaz!`,()=>{
    this.http.post<string>("Customer/Delete",{id : customer.id},res=>{
      this.swal.callToast(res,'info');
      this.getAll();
    })
  })
}

get(customer:CustomerModel){
  this.updateModel={...customer};
}

update(form:NgForm){
  if(form.valid){
    this.http.post<string>("Customer/Update",this.updateModel,res=>
    {
      this.swal.callToast(res,'info');
      this.updateModelCloseBtn?.nativeElement.click();
      this.getAll();
    }
    )
  }
}
}
