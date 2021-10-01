import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class AdminUserComponent implements OnInit {

  users: User[] = [];
  user: User = { id: 0, email: '' };

  constructor(
    private userService: UserService
  ) { }

  ngOnInit(): void {
  }
  getUsers(): void {
    this.userService.getUsers()
    .subscribe(c => this.users = c);
  }
  edit(user: User): void {
    this.user = user;
  }
  delete(user: User): void {
    if (confirm('Er du sikker på at du vil slette denne user?')){
      this.userService.deleteUser(user.id)
      .subscribe(() => {
        this.getUsers();
      })
    }
  }
  cancel(): void {
    // this.user = { id: 0, categoryId: 0, name: '', price: 0, description: '', images: [] }
  }
  save(): void{
    if (this.user.id == 0){
      this.userService.addUser(this.user)
      .subscribe(c => {
        this.users.push(c)
        // this.user = { id: 0, categoryId: 0, name: '', price: 0, description: '', images: [] }
      });
    }else {
      this.userService.updateUser(this.user.id, this.user)
      .subscribe(() => {
        // this.user = { id: 0, categoryId: 0, name: '', price: 0, description: '', images: [] }
      })
    }
  }
}
