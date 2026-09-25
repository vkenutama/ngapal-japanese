import leftButtonImg from '../assets/left.png'
import threeDotMenu from '../assets/three-dot-menu.png'

interface ITopBar {
    title: string;
    description?: string;
    icon: string
}

export default function TopBar({ title, description, icon }: ITopBar) {
    return (
        <div className="flex flex-row w-full justify-center items-center">

            {/* //Left button */}
            <div className="flex w-8.75 h-8.75 bg-white rounded-full drop-shadow-2xl justify-center align-middle ml-3">
                <img src={leftButtonImg} alt="" className="" />
            </div>

            {/* Page title bar */}
            <div className="flex flex-2 bg-white drop-shadow-2xl mx-2 h-11 justify-center items-center rounded-3xl">
                <div className="flex-1 ml-3">
                    <img className=" h-6 w-6" src={icon} alt="" />
                </div>

                <div className='flex-2 justify-center items-center align-middle text-center font-medium my-1.75'>
                    {description == undefined ?
                        <text className="">{title}</text> :

                        <div className='flex flex-col'>
                            <text className='text-[14px]'>
                                {title}
                            </text>
                            <text className='text-[10px]'>
                                {description}
                            </text>
                        </div>
                    }

                </div>

                <div className="flex-1" />
            </div>

            <div className="flex w-8.75 h-8.75 bg-white rounded-full drop-shadow-2xl justify-center items-center">
                <img src={threeDotMenu} alt="" className="h-4 w-4" />
            </div>

        </div>
    )
}