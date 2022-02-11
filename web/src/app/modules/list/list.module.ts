import { SharedModule } from 'src/app/shared/shared.module';
import { FormsModule } from '@angular/forms';
import { ListRoutingModule } from './list-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './pages/list/list.component';

@NgModule({
    declarations: [ListComponent],
    imports: [CommonModule, ListRoutingModule, FormsModule, SharedModule],
})
export class ListModule {}
