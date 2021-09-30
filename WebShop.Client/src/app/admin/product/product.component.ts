import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models';
import { ProductService } from 'src/app/_services/product.service';

@Component({
  selector: 'app-adminproduct',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class AdminProductComponent implements OnInit {

  products: Product[] = [];
  product: Product = { id: 0, categoryId: 0, name: '', price: 0, description: '', images: []  };

  constructor(
    private productService: ProductService
  ) { }  

  ngOnInit(): void {
    this.getProducts();
  }
  getProducts(): void {
    this.productService.getProducts()
    .subscribe(c => this.products = c);
  }
  edit(product: Product): void {
    this.product = product;
  }
  delete(product: Product): void {
    if (confirm('Er du sikker på at du vil slette denne Kategori')){
      this.productService.deleteProduct(product.id)
      .subscribe(() => {
        this.getProducts();
      })
    }
  }
}
