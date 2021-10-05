import { Component, OnInit } from '@angular/core';
import { Category, Product } from '../models';
import { CategoryService } from '../_services/category.service';
import { ProductService } from '../_services/product.service';
@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {

  categories: Category[] = [];
  products: Product[] = [];
    
  
  constructor(
    private categoryService:CategoryService,
    
  ) { }

  ngOnInit(): void {
    this.categoryService.getCategories()    
    .subscribe(c => {
      this.categories = c
      console.log(this.categories);
    });
    
        
  
    
  }

}
