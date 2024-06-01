import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  userForm: FormGroup;
  
  constructor(private router: Router) {
    this.userForm = new FormGroup({
      name: new FormControl(),
      email: new FormControl(),
      password: new FormControl(),
      
    });
  }


  submitForm() {
    // const user: IUser = this.userForm.value;
    // let userLoged = this.Login.ValidateUser(user);
    // if(!userLoged){
    //   alert('User not found');
    //   return;
    // }
    // window.location.href = '/dashboard';
    this.router.navigate(['/client'])
  }
  admin(){
    this.router.navigate(['/admin'])
  }
  
}
