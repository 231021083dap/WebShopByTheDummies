import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../models';
import { ProductService } from '../_services/product.service';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.scss']
})
export class ItemComponent implements OnInit {

  public itemId: any = 0;

  Item: Product = 
  {
  id : 0,
  categoryId: 0,
  name : '',
  price : 0,          
  description : ''
  };

  constructor(
    private route: ActivatedRoute,
    private itemService: ProductService
  ) { }

  ngOnInit(): void  {
    this.itemId = this.route.snapshot.paramMap.get("itemId") || 0;
    this.getItemById();
  }

  getItemById(): void {
    this.itemService.getItemById(this.itemId)
      .subscribe(a => 
      {
        this.Item = a
      });
  }
}
