import { Component } from '@angular/core';
import { LoginModel } from '../../models/login.model';
import { FormsModule, NgForm } from '@angular/forms';
import { SharedModule } from '../../modules/shared.module';
import { HttpService } from '../../services/http.service';
import { Router } from '@angular/router';
import { LoginResponseModel } from '../../models/login.response.model';
import { SwalService } from '../../services/swal.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
loginModel:LoginModel=new LoginModel();


constructor(
  private http:HttpService,
  private router:Router,
  private swal:SwalService
) {}

signIn(){
    this.http.post<LoginResponseModel>("Auth/Login",this.loginModel,res=>{
      localStorage.setItem("token",res.token);
      this.router.navigateByUrl("/");
    });
}
}
