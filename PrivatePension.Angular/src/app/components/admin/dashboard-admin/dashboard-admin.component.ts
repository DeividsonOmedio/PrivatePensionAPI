import { Component } from '@angular/core';
import { ApprovalsComponent } from '../approvals/approvals.component';
import { PurchasedComponent } from '../../clints/purchased/purchased.component';
import { ContributionsComponent } from '../contributions/contributions.component';

@Component({
  selector: 'app-dashboard-admin',
  standalone: true,
  imports: [ApprovalsComponent, PurchasedComponent, ContributionsComponent],
  templateUrl: './dashboard-admin.component.html',
  styleUrl: './dashboard-admin.component.css'
})
export class DashboardAdminComponent {

}
