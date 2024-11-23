import { Component, ElementRef, ViewChild } from '@angular/core';
import { SharedModule } from '../../modules/shared.module';
import { ProductsPipe } from '../../pipes/products.pipe';
import { SwalService } from '../../services/swal.service';
import { HttpService } from '../../services/http.service';
import { NgForm } from '@angular/forms';
import { ProductModel } from '../../models/product.model';
import { productTypeEnum } from '../../models/product.type.model';
@Component({
  selector: 'app-products',
  standalone: true,
  imports: [SharedModule,ProductsPipe],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent {
  productTypes=productTypeEnum;
  search:string="";
  products:ProductModel[]=[];
  
  createModel:ProductModel=new ProductModel();
  updateModel:ProductModel=new ProductModel();
  
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
    this.http.post<ProductModel[]>("Product/GetAll",{},res=>
    {
      this.products=res;
    }
    )
  }
  
  create(form:NgForm){
    console.log(this.createModel);
    
    if(form.valid){
      this.http.post<string>("Product/Create",this.createModel,res=>{
        this.swal.callToast(res);
        this.createModelCloseBtn?.nativeElement.click();
        this.createModel=new ProductModel();
        this.getAll();
      })
    }
  }
  
  get(product:ProductModel){
    this.updateModel=product
    this.updateModel.typeValue=product.type.value;
  }
  
  update(form:NgForm){
    if(form.valid){
      this.http.post<string>("Product/Update",this.updateModel,res=>
      {
        this.swal.callToast(res);
        this.updateModelCloseBtn?.nativeElement.click();
        this.getAll();
      }
      )
    }
  }
  
  delete(product:ProductModel){
    this.swal.callSwal("Silme Onayı!",`${product.name} isimli ürünü silmek istiyor musunuz?`,()=>{
      this.http.post<string>("Product/Delete",{id:product.id},res=>{
        this.swal.callToast(res);
        this.getAll();
      })
    })
  }
}
