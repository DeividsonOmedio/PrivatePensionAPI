import { Component } from '@angular/core';
import { IProduct } from '../../../models/product';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent {
Delete() {
throw new Error('Method not implemented.');
}
Edite() {
throw new Error('Method not implemented.');
}
  productsList: IProduct[] = [];
}
