import { Directive, Input, ElementRef, Renderer, OnInit } from '@angular/core';

@Directive({
  selector: '[appBgImage]',
})
export class BgImageDirective implements OnInit {

  @Input('appBgImage') url: string;

  constructor(private elementRef: ElementRef, private renderer: Renderer) {
  }

  ngOnInit(): void {
    this.renderer.setElementStyle(this.elementRef.nativeElement, 'background-image', `url('${this.url}')`);
  }
}
