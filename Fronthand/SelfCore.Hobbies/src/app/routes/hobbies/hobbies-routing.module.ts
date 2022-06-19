import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HobbiesBooksComponent } from './books/books.component';
import { HobbiesTravelsEditComponent } from './travels/travels-edit.component';
import { HobbiesTravelsInfoComponent } from './travels/travels-info.component';
import { HobbiesTravelsComponent } from './travels/travels.component';

const routes: Routes = [
  { path: '', redirectTo: 'travels', pathMatch: 'full' },
  { path: 'books', component: HobbiesBooksComponent },
  {
    path: 'travels',
    children: [
      { path: '', component: HobbiesTravelsComponent },
      { path: 'info', component: HobbiesTravelsInfoComponent },
      { path: 'edit', component: HobbiesTravelsEditComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HobbiesRoutingModule {}
