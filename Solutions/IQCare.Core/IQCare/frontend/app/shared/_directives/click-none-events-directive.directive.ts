import { Directive, ElementRef, Renderer } from '@angular/core';

@Directive({
    selector: '[appClickNoneEventsDirective]'
})
export class ClickNoneEventsDirectiveDirective {

    constructor(private el: ElementRef, private renderer: Renderer) {
        renderer.setElementStyle(el.nativeElement, 'pointerEvents', 'none');
    }

}
