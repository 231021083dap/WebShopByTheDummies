import { Component, OnInit } from '@angular/core';
import { Product } from '../models';
import { ProductService } from '../_services/product.service';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.scss']
})
export class FrontpageComponent implements OnInit {

  products: Product[] = [];

  constructor(
    private productService: ProductService
  ) { }

  ngOnInit(): void {
    this.productService.getProducts()
    .subscribe(a => this.products = a);
  }
}
