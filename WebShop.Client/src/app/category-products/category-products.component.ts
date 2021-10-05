import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Category, Product } from '../models';
import { CategoryService } from '../_services/category.service';

@Component({
  selector: 'app-category-products',
  templateUrl: './category-products.component.html',
  styleUrls: ['./category-products.component.scss']
})
export class CategoryProductsComponent implements OnInit {
  public categoryId: any = 0;

  Items: Product[] = [] 
  category: Category = {id:0, name: '', picture: ''}

  constructor(
    private route: ActivatedRoute,
    private categoryService: CategoryService
  ) { }

  ngOnInit(): void  {
    this.categoryId = this.route.snapshot.paramMap.get("categoryId") || 0;
    this.getProductByCategoryId();
  }

  getProductByCategoryId(): void {
    this.categoryService.getProductsByCategory(this.categoryId)
      .subscribe(a => 
      {
        this.Items = a
      }),
      this.categoryService.getCategory(this.categoryId)
      .subscribe(c => this.category = c);

  }
}
