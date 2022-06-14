import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SysUserComponent } from './user/user.component';

const routes: Routes = [{ path: 'user', component: SysUserComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SysRoutingModule {}
