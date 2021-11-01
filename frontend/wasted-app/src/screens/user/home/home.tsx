import React, {useEffect, useState} from "react"
import {ScrollView} from "react-native"
import {Navigation} from "react-native-navigation"
import {Colors, View} from "react-native-ui-lib"
import {getAllRestaurants} from "../../../api"
import {Restaurant} from "../../../api/interfaces"
import {HorizontalSection} from "../../../components/horizontal-section/horizontal-section"
import {CheapestFood} from "../../../components/sections/cheapest-food"
import {LatestFood} from "../../../components/sections/latest-food"
import {NearRestaurants} from "../../../components/sections/near-restaurants"
import {PopularRestaurants} from "../../../components/sections/popular-restaurants"
import {setHomeRoot} from "../../../services/navigation"
import {HomeProps} from "./interfaces"

export const Home = ({componentId}: HomeProps) => {
  useEffect(() => {
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
        <PopularRestaurants />
      </View>
      <View style={{borderBottomColor: Colors.grey50, borderBottomWidth: 1}}>
        <LatestFood />
      </View>
      <View style={{borderBottomColor: Colors.grey50, borderBottomWidth: 1}}>
        <NearRestaurants />
      </View>
      <View style={{borderBottomColor: Colors.grey50, borderBottomWidth: 1}}>
        <CheapestFood />
      </View>
    </ScrollView>
  )
}
