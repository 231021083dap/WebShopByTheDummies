import { Component, OnInit } from '@angular/core';

import { Product } from '../models';
import { ProductService } from '../_services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {

  products: Product[] = [];
  //product: Product = { Name: '', Description: '', Price:  };

  constructor(
    private productService: ProductService
  ) { }

  ngOnInit(): void {
    this.productService.getProducts()
      .subscribe(a => this.products = a);
  }
}