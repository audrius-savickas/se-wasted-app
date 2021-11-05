import React, {useEffect} from "react"
import {ScrollView} from "react-native"
import {Navigation} from "react-native-navigation"
import {Colors, View} from "react-native-ui-lib"
import {CheapestFood} from "../../../components/sections/cheapest-food"
import {LatestFood} from "../../../components/sections/latest-food"
import {NearRestaurants} from "../../../components/sections/near-restaurants"
import {PopularRestaurants} from "../../../components/sections/popular-restaurants"
import {setHomeRoot} from "../../../services/navigation"
import {HOME_BUTTON} from "../home-button"
import {HomeProps} from "./interfaces"

export const Home = ({componentId}: HomeProps) => {
  useEffect(() => {
    Navigation.mergeOptions(componentId, {topBar: {leftButtons: [HOME_BUTTON]}})
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "GO_BACK") {
        setHomeRoot()
      }
    })
    return () => listener.remove()
  }, [])

  return (
    <ScrollView>
      <View style={{borderBottomColor: Colors.grey50, borderBottomWidth: 1}}>
        <PopularRestaurants componentId={componentId} />
      </View>
      <View style={{borderBottomColor: Colors.grey50, borderBottomWidth: 1}}>
        <LatestFood />
      </View>
      <View style={{borderBottomColor: Colors.grey50, borderBottomWidth: 1}}>
        <NearRestaurants componentId={componentId} />
      </View>
      <View style={{borderBottomColor: Colors.grey50, borderBottomWidth: 1}}>
        <CheapestFood />
      </View>
    </ScrollView>
  )
}
