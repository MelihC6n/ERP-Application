export class MenuModel{
    name:string="";
    icon?:string="";
    url?:string="";
    isTitle:boolean=false;
    subMenus?:MenuModel[]=[];
};

export const Menus:MenuModel[]=[
    {
        name:"Ana Sayfa",
        icon:"far fa-solid fa-home",
        url:"/",
        isTitle:false,
    },
    {
        name:"Ana Grup",
        icon:"far fa-solid fa-trowel-bricks",
        url:"",
        isTitle:false,
        subMenus:[{
            name:"Müşteriler",
            icon:"far fa-solid fa-users",
            url:"/customers",
            isTitle:false
        },{
            name:"Depolar",
            icon:"fa-solid fa-warehouse",
            url:"/depots",
            isTitle:false
        },{
            name:"Ürünler",
            icon:"fa-solid fa-cubes-stacked",
            url:"/products",
            isTitle:false
        }]
    }
];