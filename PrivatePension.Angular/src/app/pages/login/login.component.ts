import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { IUser } from '../../models/user';
import { LoginApiService } from '../../services/login-api.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  userForm: FormGroup;
  
  constructor(private router: Router, private Login: LoginApiService) { 
    this.userForm = new FormGroup({
      email: new FormControl(),
      password: new FormControl(),
      
    });
  }


  submitForm() {
    const user: IUser = this.userForm.value;
    console.log(user);
    let userLoged = this.Login.login(user);
    if(!userLoged){
      alert('User not found');
      return;
    }
    //this.router.navigate(['/client'])
  }
  admin(){
    //this.router.navigate(['/admin'])
  }
  
}
