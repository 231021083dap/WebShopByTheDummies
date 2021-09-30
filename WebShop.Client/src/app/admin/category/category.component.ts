import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/models';
import { CategoryService } from 'src/app/_services/category.service';

@Component({
  selector: 'app-admincategory',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class AdminCategoryComponent implements OnInit {
  
  

  categories: Category[] = [];
  category: Category = { id: 0, name: '', picture: '' };

  constructor(
    private categoryService: CategoryService
  ) { }

  ngOnInit(): void {
    this.getCategories();
  }
  getCategories(): void {
    this.categoryService.getCategories()
    .subscribe(c => this.categories = c);
  }
  edit(category: Category): void {
    this.category = category;
  }
  delete(category: Category): void {
    if (confirm('Er du sikker på at du vil slette denne Kategori')){
      this.categoryService.deleteCategory(category.id)
      .subscribe(() => {
        this.getCategories();
      })
    }
  }
  cancel(): void {
    this.category = { id: 0, name: '', picture: '' }
  }
  save(): void{
    if (this.category.id == 0){
      this.categoryService.addCategory(this.category)
      .subscribe(c => {
        this.categories.push(c)
        this.category = { id: 0, name: '', picture: ''}
      });
    }else {
      this.categoryService.updateCategory(this.category.id, this.category)
      .subscribe(() => {
        this.category = { id: 0, name: '', picture: ''}
      })
    }
  }
}
