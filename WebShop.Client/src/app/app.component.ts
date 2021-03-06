import { Component } from '@angular/core';

import { Router } from '@angular/router';
import { Role, User } from './models';
import { AuthenticationService } from './_services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'WebShop-Client';
  currentUser: User = { id: 0, email: '', role: Role.Admin /*username: ''*/ };

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    // get the current user from authentication service
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }

  logout() {
    if (confirm('Er du sikker på du vil logge ud')) {
      // ask authentication service to perform logout
      this.authenticationService.logout();

      // subscribe to the changes in currentUser, and load Home component
      this.authenticationService.currentUser.subscribe(x => {
        this.currentUser = x
        this.router.navigate(['/']);
      });
    }
  }
}
