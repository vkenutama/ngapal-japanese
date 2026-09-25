import PageMargin from "../components/common/PageMargin"
import TopBar from "../components/TopBar"
import "../index.css"

import flashcardIcon from '../assets/flash-card.png'

export default function DeckScreenPage() {
    return (
        <PageMargin>
            <TopBar title="Mode Belajar" 
                    icon={flashcardIcon}
                    description="JLPT N1 - Hari 1"/>
            <div className="">

            </div>
        </PageMargin>
    )
}