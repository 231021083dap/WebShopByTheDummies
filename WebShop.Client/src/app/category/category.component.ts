import { Component, OnInit } from '@angular/core';
import { Category } from '../models';
@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {

  categories: Category[] = [];
  
  
  constructor() { }

  ngOnInit(): void {
    this.categories.push({ name: "Sport", picturePath: "assets/images/jakob-owens-j5kEQ1JLqZk-unsplash.jpg", id : 1} as Category);
    this.categories.push({ name: "Social", picturePath: "assets/images/michael-discenza-MxfcoxycH_Y-unsplash.jpg", id : 2} as Category);
    this.categories.push({ name: "Hangout", picturePath: "assets/images/nathan-dumlao-71u2fOofI-U-unsplash.jpg", id : 3} as Category);
    this.categories.push({ name: "Daily Tasks", picturePath: "assets/images/max-saeling-_CGxNOLM1gQ-unsplash.jpg", id :4} as Category);
  }

}
