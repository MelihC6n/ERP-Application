import { ProductTypeModel } from "./product.type.model";

export class ProductModel{
    id:string="";
    name:string="";
    type:ProductTypeModel= new ProductTypeModel;
}