import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { LoginModel } from '../../models/login.model';
import { UserModel } from '../../models/user.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginModel: LoginModel = new LoginModel();
  userModel: UserModel = new UserModel();

  constructor(
    private http: HttpClient,
    private router: Router,
  ){}
  
  login(form:NgForm){
    if (form.valid) {
      this.http.post("https://localhost:7212/api/Auth/Login", this.loginModel)
        .subscribe({
          next: (res:any) => {
            console.log(res.data);
            localStorage.setItem("token", res.data.token);
            // this.swal.callToast(res.message)
            this.router.navigateByUrl("/");
          },
          error:(err: HttpErrorResponse) => {
            console.log(err)
            // this.swal.callToast(err.error.errorMessages[0], 'warning');
          }
      });
    }
  }

  signup(form:NgForm){
    const formData = new FormData();
    if(form.valid){
      formData.append("firstName", this.userModel.firstName);
      formData.append("lastName", this.userModel.lastName);
      formData.append("userName", this.userModel.userName);
      formData.append("profilePicture", this.userModel.profilePicture);
      formData.append("email", this.userModel.email);
      formData.append("password", this.userModel.password);
      console.log(formData)
      this.http.post("https://localhost:7212/api/User/Create", formData)
      .subscribe({
        next: (res:any) => {
          console.log(res.data);
          location.reload();
        }
      })
    }
  }

  setImage(event: any){
    console.log(event);
    this.userModel.profilePicture = event.target.files[0];
  }
}
