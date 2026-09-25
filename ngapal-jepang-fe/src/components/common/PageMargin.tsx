import _, { type ReactNode } from "react";
import "../../index.css"
import auroraBg from '../../assets/aurora_bg.png'
import glow from '../../assets/glow.png'

interface ContainerProps {
    children: ReactNode;
    spacing?: string;
}

export default function PageMargin({ children, spacing = 'mx-[10px] mt-8' }: ContainerProps) {
    return (
        <div className={`flex px-2 pt-8 w-full`}>
            {/* Background */}
            <div className="absolute inset-0 h-screen w-full overflow-hidden">
                <img
                    src={auroraBg}
                    alt=""
                    className="absolute inset-0 h-full w-full object-cover opacity-5"
                />

                <img
                    src={glow}
                    alt=""
                    className="absolute inset-0 h-full w-full object-cover"
                />
            </div>
            {children}
        </div>
    )
}