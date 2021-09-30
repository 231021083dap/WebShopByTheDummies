import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ItemService } from '../_services/item.service';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.scss']
})
export class ItemComponent implements OnInit {

  public itemid: any = 0;

  constructor(
    private route: ActivatedRoute,
    private itemService: ItemService
  ) { }

  ngOnInit(): void  {
    this.itemid = this.route.snapshot.paramMap.get("itemid") || 0;

    // this.getitembyid();
  }

  // getitembyid(): void {
  //   this.itemService.GetItemById(this.itemid)
  //     .subscribe(a => 
  //     {
  //       this.Item = a
  //     });
  // }
}
