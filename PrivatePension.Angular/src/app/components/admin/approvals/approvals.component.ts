import { Component } from '@angular/core';
import { IPurchase } from '../../../models/Purchase';

@Component({
  selector: 'app-approvals',
  standalone: true,
  imports: [],
  templateUrl: './approvals.component.html',
  styleUrl: './approvals.component.css'
})
export class ApprovalsComponent {
  InAprrovals: IPurchase[] = [];
  
  Approve() {
  throw new Error('Method not implemented.');
  }
}
