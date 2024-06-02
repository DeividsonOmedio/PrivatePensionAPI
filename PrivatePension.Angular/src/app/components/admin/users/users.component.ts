import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { IUser } from '../../../models/user';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent {
  userList: IUser[] = [];
  Delete() {
  throw new Error('Method not implemented.');
  }
  Edit() {
  throw new Error('Method not implemented.');
  }
  insertInWallet() {
  throw new Error('Method not implemented.');
  }
}
