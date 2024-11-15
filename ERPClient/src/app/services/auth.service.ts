import { Injectable } from '@angular/core';
import { Route, Router } from '@angular/router';
import { jwtDecode, JwtPayload } from 'jwt-decode';
import { UserModel } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
token:string|null=null;
user:UserModel=new UserModel();

  constructor(
    private router:Router
  ) { }

  isAuthenticated(){
    this.token = localStorage.getItem("token");
    if(this.token){
      const decode:JwtPayload | any = jwtDecode(this.token); 
      const exp = decode.exp;
      const now =  new Date().getTime() / 1000;
      if(now>exp){
        this.router.navigateByUrl("/login");
        return false;
      }

      this.user.id=decode["Id"];
      this.user.name=decode["Name"];
      this.user.email=decode["Email"];
      this.user.userName=decode["UserName"];
      return true;
    }
    else{
      this.router.navigateByUrl("/login");
      return false;
    }
    
  }
}
