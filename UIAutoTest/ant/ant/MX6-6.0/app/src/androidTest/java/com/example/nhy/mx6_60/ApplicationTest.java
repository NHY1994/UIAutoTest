package com.example.nhy.mx6_60;

import android.app.Application;
import android.os.RemoteException;
import android.support.test.InstrumentationRegistry;
import android.support.test.runner.AndroidJUnit4;
import android.support.test.uiautomator.By;
import android.support.test.uiautomator.UiDevice;
import android.support.test.uiautomator.UiObject2;
import android.test.ApplicationTestCase;

import org.junit.Test;
import org.junit.runner.RunWith;

/**
 * <a href="http://d.android.com/tools/testing/testing_android.html">Testing Fundamentals</a>
 */
@RunWith(AndroidJUnit4.class)
public class ApplicationTest{
    public UiDevice device;

    @Test
    public void testDemo() throws RemoteException, InterruptedException {
        device=UiDevice.getInstance(InstrumentationRegistry.getInstrumentation());
try {
            device.wakeUp();
            Thread.sleep(500);
            device.swipe(500,1500,500,500,5);
            Thread.sleep(1000);
        } catch (RemoteException e) {
            e.printStackTrace();
        }
device.click(252,712);
Thread.sleep(3000);
device.click(821,1231);
Thread.sleep(1000);
device.click(821,1231);
Thread.sleep(1000);
device.click(247,972);
Thread.sleep(1000);
device.click(549,737);
Thread.sleep(1000);
device.click(796,1005);

	}
}