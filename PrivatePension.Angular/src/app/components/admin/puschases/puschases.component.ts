import { Component } from '@angular/core';
import { IPurchase } from '../../../models/Purchase';

@Component({
  selector: 'app-puschases',
  standalone: true,
  imports: [],
  templateUrl: './puschases.component.html',
  styleUrl: './puschases.component.css'
})
export class PuschasesComponent {
PurschaseList: IPurchase[] = []; 
}
