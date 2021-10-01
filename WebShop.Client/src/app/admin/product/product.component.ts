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
  product: Product = { id: 0, categoryId: 0, name: '', price: 0, description: '', images: [] };

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
    if (confirm('Er du sikker på at du vil slette dette produkt?')){
      this.productService.deleteProduct(product.id)
      .subscribe(() => {
        this.getProducts();
      })
    }
  }
  cancel(): void {
    this.product = { id: 0, categoryId: 0, name: '', price: 0, description: '', images: [] }
  }
  save(): void{
    if (this.product.id == 0){
      this.productService.addProduct(this.product)
      .subscribe(c => {
        this.products.push(c)
        this.product = { id: 0, categoryId: 0, name: '', price: 0, description: '', images: [] }
      });
    }else {
      this.productService.updateProduct(this.product.id, this.product)
      .subscribe(() => {
        this.product = { id: 0, categoryId: 0, name: '', price: 0, description: '', images: [] }
      })
    }
  }
}
