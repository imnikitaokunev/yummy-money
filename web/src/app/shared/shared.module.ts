import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { LoadingIndicatorSmallComponent } from './components/loading-indicator-small/loading-indicator-small.component';
import { LoadingIndicatorComponent } from './components/loading-indicator/loading-indicator.component';
import { HoverClassDirective } from './directives/hover-class.directive';
import { SidebarComponent } from './components/sidebar/sidebar.component';

@NgModule({
    imports: [CommonModule],
    declarations: [LoadingIndicatorSmallComponent, LoadingIndicatorComponent, HoverClassDirective, SidebarComponent],
    exports: [LoadingIndicatorSmallComponent, LoadingIndicatorComponent, HoverClassDirective, SidebarComponent],
})
export class SharedModule {}
