//
//  ItavioSdk.h
//  KgSdkPlugin
//
//  Created by Matthew Pichette on 2015-02-22.
//  Copyright (c) 2015 KinderGuardian Inc. All rights reserved.
//


#import <Foundation/Foundation.h>
#import <CoreData/CoreData.h>
#import "ITGlobals.h"

#ifndef ItavioSdk_ItavioApi_h
#define ItavioSdk_ItavioApi_h

@interface ITSdk : NSObject
+ (instancetype) sharedInstance;

@property (nonatomic, strong) PurchaseAttempt *inFlightPurchase;

- (void) initialize: (ITCredentials *) credentials;
- (void) initialize: (ITCredentials *) credentials onEnvironment: (int) environment;
- (void) initialize: (ITCredentials *) credentials onEnvironment: (int) environment optOut: (BOOL) shouldOptOut;

- (instancetype) initWithCredentials: (ITCredentials *) credentials;
- (instancetype) initWithCredentials: (ITCredentials *) credentials onEnvironment: (int) environment;

- (void) startDebit:(double)amount withCurrencyCode:(NSString *)currencyCode onSuccess:(ITBoolBlock)success onFailure:(ITFailureBlock)failure;

- (void) cancelDebit: (ITBoolBlock) success onFailure: (ITFailureBlock) failure;

- (void) completeDebit: (ITBoolBlock) success onFailure: (ITFailureBlock) failure;

- (BOOL) checkForParent;
- (BOOL) checkForParent: (BOOL)showGetAppDialog;
- (BOOL) checkForParent: (BOOL)showGetAppDialog shouldIgnoreSuppression: (BOOL)ignoreSuppression;

- (void) getBalance: (ITMoneyBlock) success onFailure: (ITFailureBlock) failure;
- (void) getLimit: (ITMoneyBlock) success onFailure: (ITFailureBlock) failure;

- (BOOL) isEnabled;

- (BOOL) hasLink;
- (BOOL) isLinkingComplete;

- (void) linkExternal: (BOOL) showGetAppDialog onSuccess: (ITLinkAccountBlock) success onFailure: (ITFailureBlock) failure;
- (void) linkExternal: (BOOL) showGetAppDialog shouldRelink: (BOOL) relink  onSuccess: (ITLinkAccountBlock) success onFailure: (ITFailureBlock) failure;

- (BOOL) handleLink: (NSURL *) url;

- (void) login : (NSString *) email withPassword: (NSString *) password onSuccess: (ITBoolBlock) success onFailure: (ITFailureBlock) failure;
- (void) showGetAppDialog;
- (void) showUninstalledAppDialog : (NSString *) email;
- (void) showProtected;

@end

#endif
